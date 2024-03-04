import { DataSource } from '@angular/cdk/collections';
import { catchError, finalize, map } from 'rxjs/operators';
import { Observable, of as observableOf, merge, BehaviorSubject } from 'rxjs';
import { Race } from '../../race.interface';
import { RaceService } from '../../race.service';

/**
 * Data source for the Races view. This class should
 * encapsulate all logic for fetching and manipulating the displayed data
 * (including sorting, pagination, and filtering).
 */
export class RacesDataSource extends DataSource<Race> {
  private racesSubject = new BehaviorSubject<Race[]>([]);
  private loadingSubject = new BehaviorSubject<boolean>(false);

  public loading$ = this.loadingSubject.asObservable();

  constructor(private raceService: RaceService) {
    super();
  }

  /**
   * Connect this data source to the table. The table will only update when
   * the returned stream emits new items.
   * @returns A stream of the items to be rendered.
   */
  connect(): Observable<Race[]> {
    return this.racesSubject.asObservable();
  }

  /**
   *  Called when the table is being destroyed. Use this function, to clean up
   * any open connections or free any held resources that were set up during connect.
   */
  disconnect(): void {
    this.racesSubject.complete();
    this.loadingSubject.complete();
  }

  loadRaces(): void {
    this.loadingSubject.next(true);

    this.raceService
      .getRaces()
      .pipe(
        catchError(() => observableOf([])),
        finalize(() => this.loadingSubject.next(false))
      )
      .subscribe((races) => {
        console.log(races);
        return this.racesSubject.next(races);
      });
  }
}

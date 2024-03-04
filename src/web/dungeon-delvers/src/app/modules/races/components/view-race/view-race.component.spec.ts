import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewRaceComponent } from './view-race.component';

describe('ViewRaceComponent', () => {
  let component: ViewRaceComponent;
  let fixture: ComponentFixture<ViewRaceComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ViewRaceComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ViewRaceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

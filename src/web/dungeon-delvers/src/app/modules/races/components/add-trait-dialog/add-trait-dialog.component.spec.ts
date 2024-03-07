import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddTraitDialogComponent } from './add-trait-dialog.component';

describe('AddTraitDialogComponent', () => {
  let component: AddTraitDialogComponent;
  let fixture: ComponentFixture<AddTraitDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AddTraitDialogComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AddTraitDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

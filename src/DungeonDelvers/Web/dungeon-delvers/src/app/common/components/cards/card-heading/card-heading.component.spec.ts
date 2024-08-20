import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CardHeadingComponent } from './card-heading.component';

describe('CardHeadingComponent', () => {
  let component: CardHeadingComponent;
  let fixture: ComponentFixture<CardHeadingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CardHeadingComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CardHeadingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

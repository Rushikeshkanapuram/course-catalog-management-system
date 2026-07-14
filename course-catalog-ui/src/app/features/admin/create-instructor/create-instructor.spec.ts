import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateInstructor } from './create-instructor';

describe('CreateInstructor', () => {
  let component: CreateInstructor;
  let fixture: ComponentFixture<CreateInstructor>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CreateInstructor],
    }).compileComponents();

    fixture = TestBed.createComponent(CreateInstructor);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

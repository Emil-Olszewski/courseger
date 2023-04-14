import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LessonSubjectsEditComponent } from './lesson-subjects-edit.component';

describe('LessonSubjectsEditComponent', () => {
  let component: LessonSubjectsEditComponent;
  let fixture: ComponentFixture<LessonSubjectsEditComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LessonSubjectsEditComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LessonSubjectsEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

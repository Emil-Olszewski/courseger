import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LessonSubjectFormComponent } from './lesson-subject-form.component';

describe('LessonSubjectFormComponent', () => {
  let component: LessonSubjectFormComponent;
  let fixture: ComponentFixture<LessonSubjectFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LessonSubjectFormComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LessonSubjectFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LessonSubjectListComponent } from './lesson-subject-list.component';

describe('LessonSubjectListComponent', () => {
  let component: LessonSubjectListComponent;
  let fixture: ComponentFixture<LessonSubjectListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LessonSubjectListComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LessonSubjectListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

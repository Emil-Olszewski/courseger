import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CourseEditContainerComponent } from './course-edit-container.component';

describe('CourseEditContainerComponent', () => {
  let component: CourseEditContainerComponent;
  let fixture: ComponentFixture<CourseEditContainerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CourseEditContainerComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CourseEditContainerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

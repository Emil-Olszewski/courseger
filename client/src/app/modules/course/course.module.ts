import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CoursesListPageComponent } from './pages/courses-list-page/courses-list-page.component';
import { CoursesListContainerComponent } from './containers/courses-list-container/courses-list-container.component';
import { CoursesListComponent } from "./components/courses-list/courses-list.component";
import { CourseEditPageComponent } from "./pages/course-edit-page/course-edit-page.component";
import { CourseEditContainerComponent } from "./containers/course-edit-container/course-edit-container.component";
import { CourseEditComponent } from "./components/course-edit/course-edit.component";
import { CourseRoutingModule } from "./course-routing.module";
import { LessonSubjectListComponent } from "./components/lesson-subject-list/lesson-subject-list.component";
import { SharedModule } from "../../shared/shared.module";
import { ReactiveFormsModule } from "@angular/forms";
import { CourseContainerComponent } from './containers/course-container/course-container.component';
import { CoursePageComponent } from './pages/course-page/course-page.component';
import { CourseComponent } from './components/course/course.component';
import { LessonSubjectsEditComponent } from './components/lesson-subjects-edit/lesson-subjects-edit.component';
import { LessonSubjectFormComponent } from './components/lesson-subject-form/lesson-subject-form.component';

@NgModule({
  declarations: [
    CoursesListPageComponent,
    CoursesListContainerComponent,
    CoursesListComponent,
    CourseEditPageComponent,
    CourseEditContainerComponent,
    CourseEditComponent,
    LessonSubjectListComponent,
    CourseContainerComponent,
    CoursePageComponent,
    CourseComponent,
    LessonSubjectsEditComponent,
    LessonSubjectFormComponent
  ],
  imports: [
    CommonModule,
    CourseRoutingModule,
    SharedModule,
    ReactiveFormsModule
  ]
})
export class CourseModule { }

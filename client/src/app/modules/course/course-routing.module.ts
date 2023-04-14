import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CoursesListPageComponent } from "./pages/courses-list-page/courses-list-page.component";
import { CourseEditPageComponent } from "./pages/course-edit-page/course-edit-page.component";
import { CoursePageComponent } from "./pages/course-page/course-page.component";

const routes: Routes = [
  {
    path: 'show/:id',
    component: CoursePageComponent
  },
  {
    path: 'edit/:id',
    component: CourseEditPageComponent
  },
  {
    path: '',
    component: CoursesListPageComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CourseRoutingModule { }

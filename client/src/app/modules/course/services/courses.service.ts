import { Injectable } from "@angular/core";
import { ApiService } from "../../../core/http/api.service";
import { Observable } from "rxjs";
import { ApiResponse } from "../../../shared/models/api-response";
import { Course } from "../models/course";
import { CourseExtended } from "../models/course-extended";
import {
  AddCourseRequest,
  AddCourseWithLessonSubjectsRequest,
  AddLessonSubjectToCourseRequest,
  UpdateCourseRequest, UpdateCourseWithLessonSubjectsRequest,
  UpdateLessonSubjectRequest
} from "./requests";

@Injectable({
  providedIn: 'root'
})
export class CoursesService {
  constructor(private service: ApiService) { }

  public getAllCourses(): Observable<ApiResponse<Course[]>> {
    return this.service.get<ApiResponse<Course[]>>("Course/GetAllCourses");
  }

  public getCourseById(id: string): Observable<ApiResponse<CourseExtended>> {
    return this.service.get<ApiResponse<CourseExtended>>(`Course/GetCourseById?id=${id}`);
  }

  public addCourse(request: AddCourseRequest): Observable<ApiResponse<undefined>> {
    return this.service.post('Course/AddCourse', request);
  }

  public addLessonSubjectToCourse(request: AddLessonSubjectToCourseRequest): Observable<ApiResponse<undefined>> {
    return this.service.post('Course/AddLessonSubjectToCourse', request);
  }

  public addCourseWithLessonSubjects(request: AddCourseWithLessonSubjectsRequest): Observable<ApiResponse<undefined>> {
    return this.service.post('Course/AddCourseWithLessonSubjects', request);
  }
  public updateCourse(request: UpdateCourseRequest): Observable<ApiResponse<undefined>> {
    return this.service.post('Course/UpdateCourse', request);
  }

  public updateLessonSubject(request: UpdateLessonSubjectRequest): Observable<ApiResponse<undefined>> {
    return this.service.post('Course/UpdateLessonSubject', request);
  }

  public updateCourseWithLessonSubjects(request: UpdateCourseWithLessonSubjectsRequest): Observable<ApiResponse<undefined>> {
    return this.service.post('Course/UpdateCourseWithLessonSubjects', request);
  }

  public deleteCourse(id: string): Observable<ApiResponse<undefined>> {
    return this.service.delete(`Course/DeleteCourse?id=${id}`);
  }

  public deleteLessonSubject(id: string): Observable<ApiResponse<undefined>> {
    return this.service.delete(`Course/DeleteLessonSubject?id=${id}`);
  }
}

import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { CoursesService } from "../../services/courses.service";
import { BehaviorSubject, Subject, take } from "rxjs";
import { Course } from "../../models/course";
import { ApiResponse } from "../../../../shared/models/api-response";
import { Quantity } from "../../../../shared/enums/quantity";
import { ApiErrorResponse } from "../../../../shared/models/api-error-response";
import { Router } from "@angular/router";

@Component({
  selector: 'app-courses-list-container',
  templateUrl: './courses-list-container.component.html',
  styleUrls: ['./courses-list-container.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class CoursesListContainerComponent implements OnInit {
  public courses$: Subject<Course[]> = new Subject<Course[]>();
  public error$: Subject<ApiErrorResponse | null> = new Subject<ApiErrorResponse | null>();

  constructor(private service: CoursesService, private router: Router) {
  }

  public ngOnInit(): void {
    this.getAllCourses();
  }

  public getAllCourses(): void {
    this.service.getAllCourses()
      .pipe(take(Quantity.one))
      .subscribe({
        next: response => this.courses$.next(response.body),
        error: error => this.error$.next(error)
      })
  }

  public createNewCourse(): void {
    this.router.navigate(['edit', '0']);
  }

  public showCourseDetails(courseId: string): void {
    this.router.navigate(['show', courseId]);
  }
}

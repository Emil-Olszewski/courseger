import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { BehaviorSubject, Subject, take } from "rxjs";
import { CourseExtended } from "../../models/course-extended";
import { ApiErrorResponse } from "../../../../shared/models/api-error-response";
import { ActivatedRoute, Router } from "@angular/router";
import { CoursesService } from "../../services/courses.service";
import { Quantity } from "../../../../shared/enums/quantity";

@Component({
  selector: 'app-course-container',
  templateUrl: './course-container.component.html',
  styleUrls: ['./course-container.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class CourseContainerComponent implements OnInit {
  public course$: BehaviorSubject<CourseExtended | null> = new BehaviorSubject<CourseExtended | null>(null);
  public error$: Subject<ApiErrorResponse | null> = new Subject<ApiErrorResponse | null>();
  private id!: string | null;
  constructor(private route: ActivatedRoute, private service: CoursesService, private router: Router) {
  }

  public ngOnInit(): void {
    this.route.paramMap
      .pipe(take(Quantity.one))
      .subscribe({
        next: params => {
          this.id = params.get('id');
          this.getCourse(this.id );
        }
      })
  }

  private getCourse(id: string | null): void {
    if (id && id != '0') {
      this.service.getCourseById(id)
        .pipe(take(Quantity.one))
        .subscribe({
          next: response =>  this.course$.next(response.body),
          error: error => this.error$.next(error)
        })
    }
  }

  public goBackToCoursesList(): void {
    this.router.navigateByUrl('');
  }

  public editCourse(): void {
    this.router.navigate(['..', 'edit', this.id])
  }

  public deleteCourse(): void {
    this.service.deleteCourse(this.id as string)
      .pipe(take(Quantity.one))
      .subscribe({
        next: () => this.router.navigateByUrl(''),
        error: error => this.error$.next(error)
      });
  }
}

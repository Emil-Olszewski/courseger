import { ChangeDetectionStrategy, Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from "@angular/router";
import { BehaviorSubject, Subject, take, takeUntil } from "rxjs";
import { Quantity } from "../../../../shared/enums/quantity";
import { CoursesService } from "../../services/courses.service";
import { CourseExtended } from "../../models/course-extended";
import { ApiErrorResponse } from "../../../../shared/models/api-error-response";
import { CourseEditService } from "../../services/course-edit.service";
import {
  AddCourseWithLessonSubjectsRequest,
  UpdateCourseWithLessonSubjectsRequest,
} from "../../services/requests";

@Component({
  selector: 'app-course-edit-container',
  templateUrl: './course-edit-container.component.html',
  styleUrls: ['./course-edit-container.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class CourseEditContainerComponent implements OnInit, OnDestroy {
  public course$: BehaviorSubject<CourseExtended | null> = new BehaviorSubject<CourseExtended | null>(null);
  public error$: Subject<ApiErrorResponse | null> = new Subject<ApiErrorResponse | null>();

  private id!: string | null;
  private notifier$: Subject<any> = new Subject<any>();

  constructor(private route: ActivatedRoute, private service: CoursesService,
    private router: Router, private courseEditService: CourseEditService) {
  }

  public ngOnInit(): void {
    this.route.paramMap
      .pipe(takeUntil(this.notifier$))
      .subscribe({
        next: params => this.getCourse(params.get('id'))
      })

    this.courseEditService.saveCourse$
      .pipe(takeUntil(this.notifier$))
      .subscribe(x => this.saveCourse(x));
  }

  private getCourse(id: string | null): void {
    this.id = id;
    if (id && id != '0') {
      this.service.getCourseById(id)
        .pipe(take(Quantity.one))
        .subscribe({
          next: response =>  this.course$.next(response.body),
          error: error => this.error$.next(error)
        })
    } else {
      this.course$.next({} as CourseExtended);
    }
  }

  private saveCourse(model: CourseExtended) {
    if (this.id !== '0') {
      const lessonSubjects =  !model.lessonSubjects ? [] : model.lessonSubjects.map(x => {
        return {id: x.id.includes('-') ? x.id : null, name: x.name, number: x.number}
      });

      const updateCourseRequest: UpdateCourseWithLessonSubjectsRequest = {
        id: this.id as string,
        description: model.description ?? '',
        name: model.name,
        lessonSubjects: lessonSubjects
      };

      this.service.updateCourseWithLessonSubjects(updateCourseRequest)
        .pipe(take(Quantity.one))
        .subscribe({
          next: () => this.router.navigate(['..', 'show', this.id]),
          error: error => this.error$.next(error)
        });
      return;
    }

    const lessonSubjects = !model.lessonSubjects ? [] : model.lessonSubjects.map(x => {
      return {name: x.name, number: x.number}
    });

    const addCourseRequest: AddCourseWithLessonSubjectsRequest = {
      description: model.description ?? '',
      name: model.name,
      lessonSubjects: lessonSubjects
    };

    this.service.addCourseWithLessonSubjects(addCourseRequest)
      .pipe(take(Quantity.one))
      .subscribe({
        next: () => this.router.navigateByUrl(''),
        error: error => this.error$.next(error)
      });
  }

  public ngOnDestroy(): void {
    this.notifier$.next(undefined);
    this.notifier$.complete();
  }

  public rejectChanges(): void {
    this.courseEditService.removeAllObservables();
    if (this.id && this.id != '0') {
      this.router.navigate(['..', 'show', this.id]);
    } else {
      this.router.navigateByUrl('');
    }
  }
}

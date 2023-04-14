import {
  AfterViewInit,
  ChangeDetectionStrategy,
  Component,
  EventEmitter,
  Input,
  OnDestroy,
  OnInit,
  Output
} from '@angular/core';
import { BehaviorSubject, Observable, Subject, Subscription, takeUntil } from "rxjs";
import { AbstractControl, FormControl, FormGroup, Validators } from "@angular/forms";
import { CourseExtended } from "../../models/course-extended";
import { ApiErrorResponse } from "../../../../shared/models/api-error-response";
import { CourseEditService } from "../../services/course-edit.service";

@Component({
  selector: 'app-course-edit',
  templateUrl: './course-edit.component.html',
  styleUrls: ['./course-edit.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class CourseEditComponent implements OnInit, OnDestroy {
  @Input() public course$!: Observable<CourseExtended | null>;
  @Input() public error$!: Observable<ApiErrorResponse | null>;
  @Output() public rejectChanges$: EventEmitter<any> = new EventEmitter<any>();

  private courseSubscription!: Subscription;
  private formSubscription!: Subscription;

  public form: FormGroup = new FormGroup({
    name: new FormControl('', [Validators.required, Validators.maxLength(30)]),
    description: new FormControl('', Validators.maxLength(500))
  });

  private whetherValid$!: Subject<boolean>;
  private notifier: Subject<any> = new Subject();

  constructor(private service: CourseEditService) {
  }
  handleSubmit() {
    this.service.validateAndSave();
  }

  public ngOnInit(): void {
    this.courseSubscription = this.course$.subscribe(course => {
      if (course) {
        this.form.controls['name'].setValue(course.name);
        this.form.controls['description'].setValue(course.description);
      }
    });

    this.formSubscription = this.form.valueChanges
      .subscribe(x =>  this.service.setNameAndDescription(x.name, x.description));

    this.service.validate$
      .pipe(takeUntil(this.notifier))
      .subscribe(() => this.notifyWhetherValid());

    this.whetherValid$ = this.service.getObservableToSubscribe("edit");
  }

  private notifyWhetherValid(): void {
    this.form.markAllAsTouched();
    this.whetherValid$.next(this.form.valid);
  }

  public ngOnDestroy(): void {
    this.notifier.next(undefined);
    this.notifier.complete();
  }
}

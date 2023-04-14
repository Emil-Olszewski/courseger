import { ChangeDetectionStrategy, Component, EventEmitter, Input, OnDestroy, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from "@angular/forms";
import { LessonSubject } from "../../models/course-extended";
import { Observable, Subject, takeUntil } from "rxjs";
import { CourseEditService } from "../../services/course-edit.service";

@Component({
  selector: 'app-lesson-subject-form',
  templateUrl: './lesson-subject-form.component.html',
  styleUrls: ['./lesson-subject-form.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class LessonSubjectFormComponent implements OnInit, OnDestroy{
  @Input() public lessonSubject$!: Observable<LessonSubject>;
  @Output() public removeLessonSubject$: EventEmitter<string> = new EventEmitter<string>();

  public form: FormGroup = new FormGroup({
    number: new FormControl('', [Validators.required, Validators.max(999), Validators.min(1)]),
    name: new FormControl('', [Validators.required, Validators.minLength(1) ,Validators.maxLength(40)])
  });

  private id!: string;
  private key: string = Math.random().toString();
  private whetherValid$!: Subject<boolean>;
  private notifier: Subject<any> = new Subject();

  constructor(private service: CourseEditService) {
  }
  
  public ngOnInit(): void {
    this.form.valueChanges
      .pipe(takeUntil(this.notifier))
      .subscribe(x => this.service.addOrEditLessonSubject(this.id, x.number, x.name));

    this.lessonSubject$
      .pipe(takeUntil(this.notifier))
      .subscribe(course => {
      if (course) {
        this.id = course.id;
        this.form.controls['number'].setValue(course.number);
        this.form.controls['name'].setValue(course.name);
      }
      // bez tego kliknięcie przycisku zapisz nie powoduje uruchomienia walidacji
      // z tym walidacja działa od samego początku. nie chciałem poświęcać czasu na
      // zastanawianie się czemu działa to w taki sposób, więc wybrałem opcję nr 2
      this.form.markAllAsTouched()
    });

    this.service.validate$
      .pipe(takeUntil(this.notifier))
      .subscribe(() => this.validate());

    this.whetherValid$ = this.service.getObservableToSubscribe(this.key);
  }

  private validate(): void {
    this.form.markAllAsTouched();
    this.whetherValid$.next(this.form.valid);
  }

  public ngOnDestroy(): void {
    this.notifier.next(undefined);
    this.notifier.complete();
  }

  public removeLessonSubject(): void {
    this.service.removeObservable(this.key);
    this.removeLessonSubject$.emit(this.id);
  }
}

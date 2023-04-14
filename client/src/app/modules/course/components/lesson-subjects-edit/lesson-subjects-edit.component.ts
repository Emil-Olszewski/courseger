import { ChangeDetectionStrategy, Component, Input, OnDestroy, OnInit } from '@angular/core';
import { BehaviorSubject, Observable, of, Subscription } from "rxjs";
import { CourseExtended, LessonSubject } from "../../models/course-extended";
import { CourseEditService } from "../../services/course-edit.service";

@Component({
  selector: 'app-lesson-subjects-edit',
  templateUrl: './lesson-subjects-edit.component.html',
  styleUrls: ['./lesson-subjects-edit.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class LessonSubjectsEditComponent implements OnInit, OnDestroy{
  @Input() public course$!: Observable<CourseExtended | null>;
  @Input() public sendData$!: Observable<any>;

  public subjects$: BehaviorSubject<LessonSubject[] | null> = new BehaviorSubject<LessonSubject[] | null>(null);

  private courseSubscription!: Subscription;

  constructor(private service: CourseEditService) {
  }

  public ngOnInit(): void {
    this.courseSubscription = this.course$.subscribe(course => {
      if (course?.lessonSubjects) {
        this.subjects$.next(course.lessonSubjects);
      } else {
        this.subjects$.next([]);
      }
    });
  }

  public ngOnDestroy(): void {
    if (this.courseSubscription) {
      this.courseSubscription.unsubscribe();
    }
  }

  public addNewLessonSubject(): void {
    const currentSubjects = this.subjects$.getValue();
    if (!currentSubjects) {
      return;
    }
    const newSubject = {
      // dodaje losowe ID, abym potem mógł się do nich odnosić
      id: Math.random().toString(),
      number: this.findLargestNumberIn(currentSubjects)+1,
      name: ''
    } as LessonSubject;
    currentSubjects.push(newSubject);
    this.subjects$.next(currentSubjects);
  }

  public removeLessonSubject(id: string): void {
    const currentSubjects = this.subjects$.getValue();
    if (!currentSubjects) {
      return;
    }
    const filteredSubjects = currentSubjects.filter(x => x.id !== id);
    this.subjects$.next(filteredSubjects);
    this.service.removeLessonSubject(id);
  }

  private findLargestNumberIn(subject: LessonSubject[]): number {
    const max =  Math.max(...subject.map(x => x.number));
    return 1 > max ? 0 : max;
  }

  protected readonly of = of;
}

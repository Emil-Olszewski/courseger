import { ChangeDetectionStrategy, Component, Input, OnInit } from '@angular/core';
import { Observable } from "rxjs";
import { CourseExtended } from "../../models/course-extended";

@Component({
  selector: 'app-lesson-subject-list',
  templateUrl: './lesson-subject-list.component.html',
  styleUrls: ['./lesson-subject-list.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class LessonSubjectListComponent {
  @Input() public course$!: Observable<CourseExtended | null>;
}

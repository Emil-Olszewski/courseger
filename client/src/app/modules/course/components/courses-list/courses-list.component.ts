import { ChangeDetectionStrategy, Component, EventEmitter, Input, Output } from '@angular/core';
import { Observable } from "rxjs";
import { Course } from "../../models/course";
import { ApiErrorResponse } from "../../../../shared/models/api-error-response";

@Component({
  selector: 'app-courses-list',
  templateUrl: './courses-list.component.html',
  styleUrls: ['./courses-list.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class CoursesListComponent {
  @Input() public courses$!: Observable<Course[]>;
  @Input() public error$!: Observable<ApiErrorResponse | null>;
  @Output() public createNewCourse$: EventEmitter<any> = new EventEmitter<any>();
  @Output() public showCourseDetails$: EventEmitter<string> = new EventEmitter<string>();
}

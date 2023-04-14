import { ChangeDetectionStrategy, Component, EventEmitter, Input, Output, TemplateRef } from '@angular/core';
import { Observable } from "rxjs";
import { CourseExtended } from "../../models/course-extended";
import { ApiErrorResponse } from "../../../../shared/models/api-error-response";
import { BsModalRef, BsModalService } from "ngx-bootstrap/modal";

@Component({
  selector: 'app-course',
  templateUrl: './course.component.html',
  styleUrls: ['./course.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class CourseComponent {
  public modalRef?: BsModalRef;
  @Input() public course$!: Observable<CourseExtended | null>;
  @Input() public error$!: Observable<ApiErrorResponse | null>;
  @Output() public goBackToCoursesList$: EventEmitter<any> = new EventEmitter<any>();
  @Output() public deleteCourse$: EventEmitter<any> = new EventEmitter<any>();
  @Output() public editCourse$: EventEmitter<any> = new EventEmitter<any>();

  public constructor(private modalService: BsModalService) {
  }

  public deleteCourse(): void {
    this.closeModal();
    this.deleteCourse$.emit();
  }

  public openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);
  }

  public closeModal(): void {
    this.modalRef?.hide();
  }
}

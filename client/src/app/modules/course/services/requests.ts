export interface AddCourseRequest {
  name: string;
  description: string;
}

export interface AddLessonSubjectRequest {
  number: number;
  name: string;
}

export interface AddLessonSubjectToCourseRequest extends AddLessonSubjectRequest{
  courseId: string;
}

export interface UpdateCourseRequest {
  id: string;
  name: string;
  description: string;
}

export interface UpdateLessonSubjectRequest {
  id: string;
  number: number;
  name: string;
}

export interface AddCourseWithLessonSubjectsRequest extends AddCourseRequest {
  lessonSubjects: AddLessonSubjectRequest[]
}

export interface AddOrUpdateLessonSubjectRequest {
  id: string | null;
  number: number;
  name: string;
}

export interface UpdateCourseWithLessonSubjectsRequest extends UpdateCourseRequest {
  lessonSubjects: AddOrUpdateLessonSubjectRequest[]
}


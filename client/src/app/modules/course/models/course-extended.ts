export interface CourseExtended {
  id: string;
  name: string;
  description: string;
  lessonSubjects: LessonSubject[];
}

export interface LessonSubject {
  id: string;
  number: number;
  name: string;
}

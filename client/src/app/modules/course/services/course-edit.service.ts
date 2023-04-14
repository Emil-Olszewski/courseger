import { Injectable } from "@angular/core";
import { BehaviorSubject, combineLatest, Observable, Subject, take } from "rxjs";
import { CourseExtended, LessonSubject } from "../models/course-extended";
import { Quantity } from "../../../shared/enums/quantity";

/**
 * Jest to serwis pomocniczy, który ma pozwolić na walidację, oraz zbieranie danych z wszystkich
 * kontrolek powiązanych z edycją i dodawaniem kursu.
 */
@Injectable({
  providedIn: 'root'
})
export class CourseEditService {
  private validateSubject$: Subject<any> = new Subject();
  private saveCourseSubject$: Subject<CourseExtended> = new Subject<CourseExtended>();
  private receivers: Map<string, Observable<boolean>> = new Map<string, Observable<boolean>>();
  private course$: BehaviorSubject<CourseExtended> = new BehaviorSubject<CourseExtended>({} as CourseExtended);

  /*
 Nakładka na subjecta, aby przekazać samo Observable
  */
  public get validate$(): Observable<any> {
    return this.validateSubject$.asObservable();
  }

  /*
  Nakładka na subjecta, aby przekazać samo Observable
   */
  public get saveCourse$(): Observable<CourseExtended> {
    return this.saveCourseSubject$.asObservable();
  }

  public setNameAndDescription(name: string, description: string): void {
    let course = this.course$.getValue();
    course.name = name;
    course.description = description;
    this.course$.next(course);
  }

  public addOrEditLessonSubject(id: string, number: number, name: string): void {
    let course = this.course$.getValue();
    if (!course.lessonSubjects) {
      course.lessonSubjects = [];
    }
    const lessonSubject = course.lessonSubjects.find(x => x.id === id);
    if (lessonSubject) {
      lessonSubject.number = number;
      lessonSubject.name = name;
    } else {
      const newLessonSubject = {
        id,
        number,
        name
      } as LessonSubject;
      course.lessonSubjects.push(newLessonSubject);
    }
    this.course$.next(course);
  }

  public removeLessonSubject(id: string) {
    let course = this.course$.getValue();
    course.lessonSubjects = course.lessonSubjects.filter(x => x.id !== id);
  }

  /*
  Występuje tutaj określone flow:
    1. Każda podformatka, która chce poprawnie reagować z resztą, pobiera sobie z tego serwisu Subjecta, do którego później
    wyemituje informację, czy posiada prawidłowe wartości, które mogą zostać przekazane dalej.
    2. Każda podformatka subsrybuje się też do validate$, które jest emitowane w momencie kliknięcia przycisku Zapisz.
    3. Na emisję validate$ każda podformatka powinna zwalidować samą siebie i wysłać informację o tym, czy przebiegła pomyślnie.
    4. Jeśli wszystkie podformatki, które pobrały Subjecta zwróciły prawdę, wtedy wykonywany jest zapis kursu.
  Żeby ułatwić identyfikację Subjectów posiadanych przez podformatki, ustawia się im klucz, który podformatka generuje samodzielnie.
   */
  public getObservableToSubscribe(key: string): Subject<boolean> {
    const newSubject = new Subject<boolean>();
    this.receivers.set(key, newSubject);
    return newSubject;
  }

  /*
  Jesli podformatka zostanie usunięta, potrzebne jest także usunięcie jej subjecta z listy. Jeśli się tego nie zrobi,
  serwis potem będzie czekał na pozytywną informację od formatki, która już nie istnieje i nie będzie możliwe dokonanie
  zapisu.
   */
  public removeObservable(key: string): void {
    this.receivers.delete(key);
  }

  /*
  Na sam koniec możliwe jest całkowite wyczyszczenie listy.
   */
  public removeAllObservables(): void {
    this.receivers.clear();
  }

  /*
  Ta metoda wysyła do wszystkich podpiętych validate$
   */
  public validateAndSave(): void {
    const array = Array.from(this.receivers.values());
    combineLatest(array)
      .pipe(take(Quantity.one))
      .subscribe(x => this.verifyValidationAndSave(x));

    this.validateSubject$.next(undefined);
  }

  /*
  Jeśli wszystkie zwrócone przez podformatki wartości będą prawdą, możliwe jest dokonanie zapisu. Emitowany jest wtedy
  event wyłapywany przez kontener, który się tym zajmuje.
   */
  public verifyValidationAndSave(values: boolean[]) {
    if (values.filter(x => !x).length > 0) {
      return;
    }
    this.saveCourseSubject$.next(this.course$.getValue());
    this.receivers.clear();
    this.course$.next({} as CourseExtended);

  }
}

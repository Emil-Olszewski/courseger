import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { environment } from "../../environments/environment";

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  public constructor(private http: HttpClient) {
  }

  public get<T>(path: string, params: any = new HttpParams(), responseType = 'json'): Observable<T> {
    const options = {
      observe: 'response' as const,
      params,
      responseType
    };
    return this.http.get<T>(`${environment.apiUrl}/${path}`, options as any)
      .pipe(catchError(this.formatErrors));
  }

  public put(path: string, body: any = {}): Observable<any> {
    return this.http.put(`${environment.apiUrl}/${path}`, JSON.stringify(body))
      .pipe(catchError(this.formatErrors));
  }

  public post(path: string, body: object = {}): Observable<any> {
    const options = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' })
    };
    return this.http.post(`${environment.apiUrl}/${path}`,JSON.stringify(body),options)
      .pipe(catchError(this.formatErrors));
  }

  public delete(path: string): Observable<any> {
    return this.http.delete(`${environment.apiUrl}/${path}`)
      .pipe(catchError(this.formatErrors));
  }

  private formatErrors(error: any): Observable<any> {
    console.log(error);
    return throwError(error.error);
  }
}

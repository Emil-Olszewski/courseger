import { Injectable } from '@angular/core';
import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Observable } from 'rxjs';
import { delay, finalize } from 'rxjs/operators';
import { BusyService } from "../services/busy.service";
import { Quantity } from "../../shared/enums/quantity";

@Injectable()
export class LoadingInterceptor implements HttpInterceptor {
  public constructor(private busyService: BusyService) {
  }

  public intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    this.busyService.busy();
    return next.handle(req).pipe(
      delay(Quantity.zero),
      finalize(() => {
        this.busyService.idle();
      })
    );
  }
}

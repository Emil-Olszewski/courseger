import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ErrorMessageComponent } from "./components/error-message/error-message.component";
import { CoreModule } from "../core/core.module";
import {
  FormControlValidationErrorComponent
} from "./components/form-control-validation-error/form-control-validation-error.component";
import { FormControlValidationDirective } from "./directives/form-control-validation.directive";

@NgModule({
  declarations: [
    ErrorMessageComponent,
    FormControlValidationErrorComponent,
    FormControlValidationDirective,
  ],
  exports: [
    ErrorMessageComponent,
    FormControlValidationErrorComponent,
    FormControlValidationDirective
  ],
  imports: [
    CommonModule,
    CoreModule
  ]
})
export class SharedModule { }

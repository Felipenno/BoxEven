import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { InputDateComponent } from './input-date/input-date.component';
import { InputNumberComponent } from './input-number/input-number.component';
import { InputSelectComponent } from './input-select/input-select.component';
import { InputTextComponent } from './input-text/input-text.component';
import { InputTextareaComponent } from './input-textarea/input-textarea.component';
import { CamposService } from './campos.service';



@NgModule({
  declarations: [
    InputDateComponent,
    InputNumberComponent,
    InputSelectComponent,
    InputTextComponent,
    InputTextareaComponent
  ],
  imports: [
    CommonModule
  ],
  exports:[
    InputDateComponent,
    InputNumberComponent,
    InputSelectComponent,
    InputTextComponent,
    InputTextareaComponent
  ],
  providers: [CamposService]
})
export class CamposModule { }

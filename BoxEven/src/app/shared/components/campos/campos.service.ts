import { Injectable } from '@angular/core';
import { AbstractControl, FormGroup } from '@angular/forms';

@Injectable({
  providedIn: 'root'
})
export class CamposService {

  constructor() { }

  contemErros(control: AbstractControl, errorName: string) : boolean {
    return control.hasError(errorName);
  }

  contemErrosValidacao(control: AbstractControl, errorName: string): boolean{
    if ((control.dirty || control.touched) && this.contemErros(control, errorName)){
      return true;
    }
    else{
      return false;
    }
  }

  btnFormIsValid(formGroup: FormGroup){
    return !formGroup.invalid;
  }

  // lengthValidator(control: AbstractControl, errorName: string): number {
  //   const error = control.errors ?? [errorName];
  //   return error.requiredLength || error.min || error.max || 0;
  // }
}

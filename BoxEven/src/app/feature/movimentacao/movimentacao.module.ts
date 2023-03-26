import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CriarMovimentacaoComponent } from './criar-movimentacao/criar-movimentacao.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';



@NgModule({
  declarations: [
    CriarMovimentacaoComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
  ]
})
export class MovimentacaoModule { }

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ListarPedidoComponent } from './listar-pedido/listar-pedido.component';
import { CamposModule } from 'src/app/shared/components/campos/campos.module';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';



@NgModule({
  declarations: [
    ListarPedidoComponent
  ],
  imports: [
    CommonModule,
    CamposModule,
    ReactiveFormsModule,
    FormsModule,
  ]
})
export class SeparacaoModule { }

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ListarPedidoComponent } from './listar-pedido/listar-pedido.component';
import { CamposModule } from 'src/app/shared/components/campos/campos.module';



@NgModule({
  declarations: [
    ListarPedidoComponent
  ],
  imports: [
    CommonModule,
    CamposModule
  ]
})
export class SeparacaoModule { }

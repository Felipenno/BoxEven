import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ListarLocalizacaoComponent } from './listar-localizacao/listar-localizacao.component';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ProdutoService } from '../produto/produto.service';



@NgModule({
  declarations: [
    ListarLocalizacaoComponent,
  ],
  imports: [
    CommonModule,
    RouterModule,
    ReactiveFormsModule,
    FormsModule
  ],
  providers: [ProdutoService]
})
export class LocalizacaoModule { }

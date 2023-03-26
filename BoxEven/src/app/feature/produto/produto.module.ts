import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ListarProdutoComponent } from './listar-produto/listar-produto.component';
import { CriarProdutoComponent } from './criar-produto/criar-produto.component';
import { EditarProdutoComponent } from './editar-produto/editar-produto.component';
import { ProdutoService } from './produto.service';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { PaginacaoComponent } from 'src/app/shared/components/paginacao/paginacao.component';



@NgModule({
  declarations: [
    ListarProdutoComponent,
    CriarProdutoComponent,
    EditarProdutoComponent,
    PaginacaoComponent
  ],
  imports: [
    RouterModule,
    ReactiveFormsModule,
    FormsModule,
    CommonModule
  ],
  providers: [ProdutoService]
})
export class ProdutoModule { }

import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ListarLocalizacaoComponent } from './localizacao/listar-localizacao/listar-localizacao.component';
import { MainComponent } from './main.component';
import { CriarMovimentacaoComponent } from './movimentacao/criar-movimentacao/criar-movimentacao.component';
import { CriarProdutoComponent } from './produto/criar-produto/criar-produto.component';
import { EditarProdutoComponent } from './produto/editar-produto/editar-produto.component';
import { ListarProdutoComponent } from './produto/listar-produto/listar-produto.component';
import { ListarPedidoComponent } from './separacao/listar-pedido/listar-pedido.component';

const routes: Routes = [
  {
    path: '',
    component: MainComponent,
    children: [
      { path: '', component: ListarPedidoComponent, pathMatch: 'full' },
      { 
        path: 'produto',
        children: [
          { path: '', component: ListarProdutoComponent, pathMatch: 'full'},
          { path: 'cadastro', component: CriarProdutoComponent, pathMatch: 'full'},
          { path: 'editar/:id', component: EditarProdutoComponent, pathMatch: 'full'}
        ]
      },
      { 
        path: 'localizacao',
        children:[
          { path: '', component: ListarLocalizacaoComponent, pathMatch: 'full'},
        ] 
      },
      { path: 'movimentacao', component: CriarMovimentacaoComponent, pathMatch: 'full'},
    ]
  },

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MainRoutingModule { }

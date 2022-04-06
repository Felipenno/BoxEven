import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ListarLocalizacaoComponent } from './feature/localizacao/listar-localizacao/listar-localizacao.component';
import { MapaLocalizacaoComponent } from './feature/localizacao/mapa-localizacao/mapa-localizacao.component';
import { CriarMovimentacaoComponent } from './feature/movimentacao/criar-movimentacao/criar-movimentacao.component';
import { CriarProdutoComponent } from './feature/produto/criar-produto/criar-produto.component';
import { EditarProdutoComponent } from './feature/produto/editar-produto/editar-produto.component';
import { ListarProdutoComponent } from './feature/produto/listar-produto/listar-produto.component';
import { GerarRelatorioComponent } from './feature/relatorio/gerar-relatorio/gerar-relatorio.component';
import { ListarPedidoComponent } from './feature/separacao/listar-pedido/listar-pedido.component';
import { CadastrarComponent } from './feature/usuario/cadastrar/cadastrar.component';
import { LoginComponent } from './feature/usuario/login/login.component';

const routes: Routes = [
  { path: '', redirectTo: 'usuario/login', pathMatch: 'full' },
  { path: 'separacao', component: ListarPedidoComponent, pathMatch: 'full' },
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
      { path: 'mapa', component: MapaLocalizacaoComponent, pathMatch: 'full'},
    ] 
  },
  { path: 'movimentacao', component: CriarMovimentacaoComponent, pathMatch: 'full'},
  { path: 'relatorio', component: GerarRelatorioComponent, pathMatch: 'full'},
  {
    path: 'usuario',
    children: [
      { path: 'cadastro', component: CadastrarComponent, pathMatch: 'full'},
      { path: 'login', component: LoginComponent, pathMatch: 'full'}
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

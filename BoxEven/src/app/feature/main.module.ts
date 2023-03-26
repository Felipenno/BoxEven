import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MainRoutingModule } from './main-routing.module';
import { MainComponent } from './main.component';
import { NavComponent } from '../shared/components/nav/nav.component';
import { RodapeComponent } from '../shared/components/rodape/rodape.component';
import { MovimentacaoModule } from './movimentacao/movimentacao.module';
import { ProdutoModule } from './produto/produto.module';
import { SeparacaoModule } from './separacao/separacao.module';
import { LocalizacaoModule } from './localizacao/localizacao.module';


@NgModule({
  declarations: [
    MainComponent,
    RodapeComponent,
    NavComponent  
  ],
  imports: [
    CommonModule,
    LocalizacaoModule,
    MainRoutingModule,
    MovimentacaoModule,
    ProdutoModule,
    SeparacaoModule,
  ]
})
export class MainModule { }

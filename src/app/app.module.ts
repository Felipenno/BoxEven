import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AlertaComponent } from './shared/components/alerta/alerta.component';
import { RodapeComponent } from './shared/components/rodape/rodape.component';
import { TabelaComponent } from './shared/components/tabela/tabela.component';
import { NavComponent } from './shared/components/nav/nav.component';
import { ProdutoModule } from './feature/produto/produto.module';
import { LocalizacaoModule } from './feature/localizacao/localizacao.module';
import { MovimentacaoModule } from './feature/movimentacao/movimentacao.module';
import { RelatorioModule } from './feature/relatorio/relatorio.module';
import { SeparacaoModule } from './feature/separacao/separacao.module';
import { UsuarioModule } from './feature/usuario/usuario.module';
import { HttpClientModule } from '@angular/common/http';

@NgModule({
  declarations: [
    AppComponent,
    AlertaComponent,
    RodapeComponent,
    TabelaComponent,
    NavComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    ProdutoModule,
    LocalizacaoModule,
    MovimentacaoModule,
    RelatorioModule,
    SeparacaoModule,
    UsuarioModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }

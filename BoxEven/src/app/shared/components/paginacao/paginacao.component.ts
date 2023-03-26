import { Component, Input, OnInit } from '@angular/core';
import { PaginacaoService } from './paginacao.service';

@Component({
  selector: 'app-paginacao',
  templateUrl: './paginacao.component.html',
  styleUrls: ['./paginacao.component.scss']
})
export class PaginacaoComponent implements OnInit {
  private paginaAtual = 1;

  cssNumero = 'bi bi-1-circle-fill';
  paginaFinal = false;
  primeiraPagina = true;

  @Input() itensPorPagina = 0;

  constructor(private paginacaoService: PaginacaoService) { }

  ngOnInit(): void {
    this.paginacaoService.obterPaginaAtual().subscribe(p => this.paginaAtual = p);
    this.paginacaoService.obterTamanhoLista().subscribe(x => this.habilitarDesabilitarBotoes(x));
  }

  private definirClasseNumero(): void{
    this.cssNumero = `bi bi-${this.paginaAtual}-circle-fill`
  }

  private habilitarDesabilitarBotoes(tamanhoLista: number): void{
    this.primeiraPagina = this.paginaAtual <= 1;
    this.paginaFinal = tamanhoLista < this.itensPorPagina || tamanhoLista <= 0;
  }

  voltarPagina(){
    this.paginacaoService.mudarPagina(this.paginaAtual - 1)
    this.definirClasseNumero();
  }

  proximaPagina(){
    this.paginacaoService.mudarPagina(this.paginaAtual + 1)
    this.definirClasseNumero();
  }

}

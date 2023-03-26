import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PaginacaoService {
  private paginacaoSubject = new BehaviorSubject<number>(1);
  private tamanhoListaSubject = new BehaviorSubject<number>(1);

  iniciarPaginacao(){
    this.paginacaoSubject.next(1);
  }

  obterPaginaAtual(): BehaviorSubject<number>{
    return this.paginacaoSubject;
  }

  mudarPagina(pagina: number){
    this.paginacaoSubject.next(pagina);
  }

  obterTamanhoLista():BehaviorSubject<number>{
    return this.tamanhoListaSubject;
  }

  mudarTamanhoLista(tamanho : number): void{
    this.tamanhoListaSubject.next(tamanho)
  }
}

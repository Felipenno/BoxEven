import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ComunicacaoService } from 'src/app/core/services/comunicacao.service';
import { ParamsModel } from 'src/app/shared/models/params-model';
import { Pedido } from 'src/app/shared/models/pedido';

@Injectable({
  providedIn: 'root'
})
export class SeparacaoService {

  private  endPointUrl = 'pedido';

  constructor(private comunicacaoService: ComunicacaoService ) { }

  listarPedidos() : Observable<Pedido[]>{
    return this.comunicacaoService.listar<Pedido[]>(this.endPointUrl).pipe();
  }

  listarPedidosFiltro(paramsList?: ParamsModel[]) : Observable<Pedido[]>{
    return this.comunicacaoService.listar<Pedido[]>(this.endPointUrl + '/filtro', paramsList).pipe();
  }

  atualizarStatus(status: number, pedidoId: string){
    return this.comunicacaoService.patchAny(this.endPointUrl, pedidoId, status).pipe();
  }
}

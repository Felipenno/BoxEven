import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ComunicacaoService } from 'src/app/core/services/comunicacao.service';
import { ParamsModel } from 'src/app/shared/models/params-model';
import { Pedido } from 'src/app/shared/models/pedido';
import { PedidoAlterarStatus } from 'src/app/shared/models/pedido-alterar-status';

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

  atualizarStatus(pedidoAlterarStatus: PedidoAlterarStatus): Observable<PedidoAlterarStatus>{
    return this.comunicacaoService.patch<PedidoAlterarStatus>(this.endPointUrl, pedidoAlterarStatus).pipe();
  }
}

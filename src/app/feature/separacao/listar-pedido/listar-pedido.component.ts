import { Component, OnInit } from '@angular/core';
import { Pedido } from 'src/app/shared/models/pedido';
import { SeparacaoService } from '../separacao.service';

@Component({
  selector: 'app-listar-pedido',
  templateUrl: './listar-pedido.component.html',
  styleUrls: ['./listar-pedido.component.scss']
})
export class ListarPedidoComponent implements OnInit {

  pedidos: Pedido[] = [];

  readonly semImagem: string = './assets/img/no-img.png';

  constructor(private separacaoService: SeparacaoService) { }

  ngOnInit(): void {
    this.listarPedidos();
  }

  listarPedidos(): void{
    this.separacaoService.listarPedidos().subscribe({
      next: dados => {this.pedidos = dados, console.log(dados)},
      error: err => console.log(err)
    })
  }

  alterarStatus(status: number, pedidoId: string):void{
    this.separacaoService.atualizarStatus(status, pedidoId).subscribe({
      next: dados => console.log(dados),
      error: err => console.log(err)
    })
  }

}

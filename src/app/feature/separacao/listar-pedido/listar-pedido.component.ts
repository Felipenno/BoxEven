import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormArray, FormControl, Validators } from '@angular/forms';
import { Pedido } from 'src/app/shared/models/pedido';
import { PedidoAlterarStatus } from 'src/app/shared/models/pedido-alterar-status';
import { ProdutoAlterarQuantidade } from 'src/app/shared/models/produto-alterar-quantidade';
import { ProdutoService } from '../../produto/produto.service';
import { SeparacaoService } from '../separacao.service';

@Component({
  selector: 'app-listar-pedido',
  templateUrl: './listar-pedido.component.html',
  styleUrls: ['./listar-pedido.component.scss']
})
export class ListarPedidoComponent implements OnInit {
  pedidos: Pedido[] = [];
  formArray = new FormArray([]);

  readonly semImagem: string = './assets/img/no-img.png';

  constructor(
    private separacaoService: SeparacaoService,
    private fb: FormBuilder
    
  ) {}

  ngOnInit(): void {
    this.listarPedidos();
  }

  private buildForm(){
    this.formArray = new FormArray([]);
    this.pedidos.forEach((pedido, peindex) => {
      let formBuilder = this.fb.group({});

      pedido.produtos.forEach((value, prindex) => {
        formBuilder.addControl(`${peindex}.${prindex}`, new FormControl(false, Validators.requiredTrue))
      })

      this.formArray.push(formBuilder)
    })
  }

  public getFormGroup(index: number) : FormGroup{
    return this.formArray.at(index) as FormGroup;
  }

  public getFormControl(indexGroup: number, indexControl: number) : FormControl{
    let controlName = `${indexGroup}.${indexControl}`;
    let form =  this.formArray.at(indexGroup) as FormGroup
    return form.controls[controlName] as FormControl;
  }

  listarPedidos(): void{
    this.separacaoService.listarPedidos().subscribe({
      next: dados => {this.pedidos = dados, console.log(dados), this.buildForm()},
      error: err => console.log(err)
    })
  }

  concluirPedido(pedidoId: string):void{
    let pedido = this.pedidos.find(x => x.id = pedidoId) ?? new Pedido();
    let produtos: ProdutoAlterarQuantidade[] = []
    
    pedido.produtos.forEach((p) => {
      produtos.push({idProduto: p.produtoId, quantidade: p.quantidade ?? 0})
    })

    let pedidoAlterarStatus: PedidoAlterarStatus = {
      idPedido: pedido.id,
      idUsuario: localStorage.getItem("idUsuario") ?? '',
      numeroPedido: pedido.numero,
      produtos: produtos,
      statusPedido: 2
    }

    console.log('status', pedidoAlterarStatus)

    this.separacaoService.atualizarStatus(pedidoAlterarStatus).subscribe({
      next: dados => { console.log(dados), this.listarPedidos()},
      error: err => console.log(err)
    })
  }

  cancelarPedido(pedidoId: string):void{
    let pedidoAlterarStatus: PedidoAlterarStatus = {
      idPedido: pedidoId,
      statusPedido: 3
    }

    console.log('status', pedidoAlterarStatus)

    this.separacaoService.atualizarStatus(pedidoAlterarStatus).subscribe({
      next: dados => { console.log(dados), this.listarPedidos()},
      error: err => console.log(err)
    })
  }
}
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormArray, FormControl, Validators } from '@angular/forms';
import { Pedido } from 'src/app/shared/models/pedido';
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
    this.separacaoService.atualizarStatus(2, pedidoId).subscribe({
      next: dados => { console.log(dados), this.listarPedidos()},
      error: err => console.log(err)
    })
  }

  cancelarPedido(pedidoId: string):void{
    this.separacaoService.atualizarStatus(3, pedidoId).subscribe({
      next: dados => { console.log(dados), this.listarPedidos()},
      error: err => console.log(err)
    })
  }
}
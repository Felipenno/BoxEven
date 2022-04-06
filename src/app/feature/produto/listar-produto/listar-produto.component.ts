import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ParamsModel } from 'src/app/shared/models/params-model';
import { Produto } from 'src/app/shared/models/produto';
import { ProdutoService } from '../produto.service';

@Component({
  selector: 'app-listar-produto',
  templateUrl: './listar-produto.component.html',
  styleUrls: ['./listar-produto.component.scss']
})
export class ListarProdutoComponent implements OnInit {

  produtos: Produto[] = [];
  filtroForm!: FormGroup;

  readonly semImagem: string = './assets/img/no-img.png';
  
  constructor(
    private produtoService: ProdutoService,
    private fb: FormBuilder
  ) { }

  ngOnInit(): void {
    this.listar();
    this.validacaoFiltro();
  }

  private validacaoFiltro(): void {
    this.filtroForm = this.fb.group({
      codigo: ['', [Validators.min(1)]],
      descricao: ['', [Validators.minLength(3), Validators.maxLength(30)]],
      status: ['']
    });
  }

  listar(): void {
    this.produtoService.listarProdutos().subscribe({
      next: data => { this.produtos = data},
      error: err => { console.log(err) }
    })
  }

  submitForm(): void {
    let parametros: ParamsModel[] = [];
    let codigo = this.filtroForm.get('codigo')?.value;
    let descricao = this.filtroForm.get('descricao')?.value;
    let status = this.filtroForm.get('status')?.value;

    if (codigo) {
      parametros.push({ nome: 'id', valor: codigo })
    }

    if (descricao) {
      parametros.push({ nome: 'descricao', valor: descricao });
    }

    if (status === 'ativo') {
      parametros.push({ nome: 'status', valor: true })
    }
    else if (status === 'inativo') {
      parametros.push({ nome: 'status', valor: false })
    }

    this.produtoService.listarProdutos(parametros).subscribe({
      next: data => { this.produtos = data },
      error: err => { console.log(err) }
    });
  }

}

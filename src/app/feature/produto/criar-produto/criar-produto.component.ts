import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ProdutoService } from '../produto.service';
import { ProdutoNovo } from 'src/app/shared/models/produto-novo';

@Component({
  selector: 'app-criar-produto',
  templateUrl: './criar-produto.component.html',
  styleUrls: ['./criar-produto.component.scss']
})
export class CriarProdutoComponent implements OnInit {

  produto!: ProdutoNovo;
  produtoForm!: FormGroup;

  readonly semImagem: string = './assets/img/no-img.png';
  inputImagem: any = this.semImagem;
  imagemDefinida: boolean = false;

  constructor(
    private produtoService: ProdutoService,
    private fb: FormBuilder
  ) { }

  ngOnInit(): void {
    this.validacaoProduto();
  }

  private validacaoProduto(): void {
    this.produtoForm = this.fb.group({
      ativo: [ true, [Validators.required]],
      quantidade: ['', [Validators.required, Validators.min(0), Validators.max(9999)]],
      preco: ['', [Validators.required, Validators.min(5), Validators.max(10000)]],
      nome: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(40)]],
      codigoBarras: ['', [Validators.minLength(5)]],
      unidadeMedidaId: ['1', [Validators.required]]
    });
  }

  definirImagem(ev:any): void{
    const reader = new FileReader();

    if(ev.target.files.length === 1){
      this.inputImagem = ev.target.files[0];
      reader.readAsDataURL(this.inputImagem);
      reader.onload = (event:any) => this.inputImagem = event.target.result;
      this.imagemDefinida = true;
    }
  }

  removerImg(){
    this.inputImagem = this.semImagem;
    this.imagemDefinida = false;
  }

  submitForm(){
    this.produto = this.produtoForm.value;

    if(this.imagemDefinida){
      this.produto.imagem = this.inputImagem;
    }

    console.log(this.produto)

    this.produtoService.cadastrarProduto(this.produto).subscribe({
      next: data => { console.log(data)},
      error: err => { console.log(err)}
    })
  }
}

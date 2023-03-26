import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ProdutoService } from '../produto.service';
import { ProdutoNovo } from 'src/app/shared/models/produto-novo';
import { AlertaService } from 'src/app/shared/components/alerta/alerta.service';

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
    private fb: FormBuilder,
    private alertaService: AlertaService
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

    (this.produto)

    this.produtoService.cadastrarProduto(this.produto).subscribe({
      next: res => { 
        if(res){
          if(res.sucesso){
            this.alertaService.sucesso(res.mensagem)
            this.produtoForm.reset({ativo: true, unidadeMedidaId: '1'});
          }
          else{
            this.alertaService.erro(res.mensagem)
          }
        }
      }
    })
  }
}

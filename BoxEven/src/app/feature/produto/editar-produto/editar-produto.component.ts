import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { AlertaService } from 'src/app/shared/components/alerta/alerta.service';
import { LocalizacaoEndereco } from 'src/app/shared/models/localizacao-endereco';
import { ProdutoEditar } from 'src/app/shared/models/produto-editar';
import { LocalizacaoService } from '../../localizacao/localizacao.service';
import { ProdutoService } from '../produto.service';

@Component({
  selector: 'app-editar-produto',
  templateUrl: './editar-produto.component.html',
  styleUrls: ['./editar-produto.component.scss']
})
export class EditarProdutoComponent implements OnInit {

  produto!: ProdutoEditar;
  produtoForm!: FormGroup;
  routerId: any;
  localizacoes: LocalizacaoEndereco[] = [];
  localizacoesDisponiveis: LocalizacaoEndereco[] = [];

  readonly semImagem: string = './assets/img/no-img.png';
  inputImagem: any = this.semImagem;
  imagemDefinida: boolean = false;

  constructor(
    private produtoService: ProdutoService,
    private localizacaoService: LocalizacaoService,
    private alertaService: AlertaService,
    private fb: FormBuilder,
    private activatedRoute: ActivatedRoute
  ) { }

  ngOnInit(): void {
    this.validacaoProduto();
    this.buscarProduto();
    this.buscarLocalizacoes();
  }

  private validacaoProduto(): void {
    this.produtoForm = this.fb.group({
      produtoId:[''],
      quantidade: ['', [Validators.required, Validators.min(1), Validators.max(9999)]],
      ativo: ['', [Validators.required]],
      preco: ['', [Validators.required, Validators.min(5), Validators.max(10000)]],
      nome: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(40)]],
      codigoBarras: ['', [Validators.minLength(5)]],
      unidadeMedidaId: ['', [Validators.required]]
    });

    this.produtoForm.controls['produtoId'].disable();
    this.produtoForm.controls['quantidade'].disable();
  }

  buscarProduto(): void{
    this.routerId = this.activatedRoute.snapshot.paramMap.get('id');

    this.produtoService.listarProdutoPorId(this.routerId).subscribe({
      next: res => { 
        if(res && res.objeto){
          if(res.sucesso){
            this.produto = {
              produtoId: res.objeto.produtoId,
              ativo: res.objeto.ativo,
              quantidade: res.objeto.quantidade,
              preco: res.objeto.preco,
              nome: res.objeto.nome,
              imagem: res.objeto.imagem,
              codigoBarras: res.objeto.codigoBarras,
              unidadeMedidaId: res.objeto.unidadeMedida.unidadeMedidaId
            }
    
            this.produtoForm.patchValue(this.produto);
            if(this.produto.imagem){
              this.inputImagem = this.produto.imagem
              this.imagemDefinida = true;
            }
    
            this.localizacoes = res.objeto.localizacoes ?? [];
    
          }
          else{
            this.alertaService.erro(res.mensagem)
          }
        }
      }
    });
  }

  buscarLocalizacoes(): void {
    this.localizacaoService.listarLocalizacoesDisponiveis().subscribe({
      next: res => { 
        if(res && res.objeto){
          if(res.sucesso){
            this.localizacoesDisponiveis = res.objeto;
          }
          else{
            this.alertaService.erro(res.mensagem)
          }
        }
      }
    })
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
    this.produto.imagem = null;
    this.inputImagem = this.semImagem;
    this.imagemDefinida = false;
  }

  limparCampos(){
    this.removerImg();
    this.produtoForm.get('ativo')?.reset();
    this.produtoForm.get('preco')?.reset();
    this.produtoForm.get('nome')?.reset();
    this.produtoForm.get('codigoBarras')?.reset();
    this.produtoForm.get('unidadeMedidaId')?.reset();
  }

  submitForm(){
    let form = this.produtoForm.getRawValue() as ProdutoEditar;
    this.produto.ativo = form.ativo;
    this.produto.quantidade = form.quantidade;
    this.produto.preco = form.preco;
    this.produto.nome = form.nome;
    this.produto.imagem = form.imagem;
    this.produto.codigoBarras = form.codigoBarras;
    this.produto.unidadeMedidaId = form.unidadeMedidaId;
    this.produto.localizacoes = [];

    this.localizacoes.forEach( (id) => {
      this.produto.localizacoes?.push(id.localizacaoId);
    })

    if(this.imagemDefinida){
      this.produto.imagem = this.inputImagem;
    }

    this.produtoService.atualizarProduto(this.produto, this.routerId).subscribe({
      next: res => { 
        if(res){
          if(res.sucesso){
            this.alertaService.sucesso(res.mensagem);
          }
          else{
            this.alertaService.erro(res.mensagem);
          }
        }
      }
    })
  }

  gerarNumeros(limite: number): number[]{
    let numeros: number[] = [];

    for (let index = 1; index <= limite; index++) {
      numeros.push(index);
    }

    return numeros;
  }

  adicionarEndereco(id:number){
    let endereco: any = this.localizacoesDisponiveis.find(item => item.localizacaoId === id)
    this.localizacoes.push(endereco)

    let enderecoIndex = this.localizacoesDisponiveis.findIndex(item => item.localizacaoId === id)
    this.localizacoesDisponiveis.splice(enderecoIndex, 1);
  }

  removerEndereco(id:number){
    let endereco: any = this.localizacoes.find(item => item.localizacaoId === id)
    this.localizacoesDisponiveis.unshift(endereco)

    let enderecoIndex = this.localizacoes.findIndex(item => item.localizacaoId === id)
    this.localizacoes.splice(enderecoIndex, 1);
  }
}

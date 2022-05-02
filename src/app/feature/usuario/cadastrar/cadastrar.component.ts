import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { Router } from '@angular/router';
import { Usuario } from 'src/app/shared/models/usuario';
import { UsuarioService } from '../usuario.service';

@Component({
  selector: 'app-cadastrar',
  templateUrl: './cadastrar.component.html',
  styleUrls: ['./cadastrar.component.scss']
})
export class CadastrarComponent implements OnInit {

  usuarioForm!: FormGroup;

  constructor(
    private router : Router,
    private usuarioService: UsuarioService,
    private fb: FormBuilder
  ) { }

  ngOnInit(): void {
    this.validacaoForm();
  }

  private validacaoForm(): void{
    this.usuarioForm = this.fb.group({
      nome: ['', [Validators.required, Validators.minLength(10), Validators.maxLength(40)]],
      email: ['', [Validators.required, Validators.minLength(15), Validators.maxLength(40)]],
      senha: ['', [Validators.required, Validators.minLength(8), Validators.maxLength(40)]],
      rsenha: ['', [Validators.required, Validators.minLength(8), Validators.maxLength(40)]]
    })
  }

  private conferirSenhas(control: FormControl){
    let senha = this.usuarioForm.get('senha')?.value;
    let rsenha = control?.value;

    if(senha && rsenha){
      if(rsenha !== senha){
        return {senhasDiferentes: true};
      }
    }

    return null;
  }

  cadastrar(){
    this.router.navigateByUrl('/usuario/login')
  }

  submitForm(): void{
    let usuario: Usuario = this.usuarioForm.value;
    console.log(usuario)
    console.log(this.usuarioForm.value);

    this.usuarioService.login(usuario).subscribe({
      next: dados => console.log(dados),
      error: err => console.log(err)
    })
  }

}

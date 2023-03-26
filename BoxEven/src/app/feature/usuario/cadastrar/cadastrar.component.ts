import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl, AbstractControl } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/core/services/auth.service';
import { AlertaService } from 'src/app/shared/components/alerta/alerta.service';
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
    private authService: AuthService,
    private fb: FormBuilder,
    private alertaService: AlertaService
  ) { }

  ngOnInit(): void {
    if(this.authService.usuarioEstaAutenticado()){
      this.router.navigateByUrl('/sistema');
    }
    this.validacaoForm();
  }

  private validacaoForm(): void{
    this.usuarioForm = this.fb.group({
      nome: ['', [Validators.required, Validators.minLength(10), Validators.maxLength(40)]],
      email: ['', [Validators.required, Validators.minLength(15), Validators.maxLength(40)]],
      senha: ['', [Validators.required, Validators.minLength(8), Validators.maxLength(40)]],
      rsenha: ['', [Validators.required, Validators.minLength(8), Validators.maxLength(40)]]
    })

    //this.usuarioForm.setValidators();
  }

  private conferirSenhas(control: AbstractControl){
    let senha = control.get('senha')?.value;
    let rsenha = control.get('rsenha')?.value;
    (senha);
    (rsenha);
    
    if(senha && rsenha){
      ('entro1')
      if(rsenha !== senha){
        ('entro2')
        return {senhasDiferentes: true};
      }
     ('naoen')
    }
    ('fora')
    return null;
  }

  cadastrar(){
    let usuario: Usuario = this.usuarioForm.value;

    this.usuarioService.registrar(usuario).subscribe({
      next: dados => {
        if(dados){
          if(dados.sucesso){
            this.alertaService.sucesso(dados.mensagem)
            this.router.navigateByUrl('/usuario');
          }
          else{
            this.alertaService.erro(dados.mensagem)
          }
        }
      }
    })
  }
}

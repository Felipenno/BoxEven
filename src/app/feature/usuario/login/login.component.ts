import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Usuario } from 'src/app/shared/models/usuario';
import { UsuarioService } from '../usuario.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
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
      apelido: ['', [Validators.required, Validators.minLength(10), Validators.maxLength(10)]],
      senha: ['', [Validators.required, Validators.minLength(8), Validators.maxLength(40)]],
    })
  }

  submitForm(): void{
    let usuario: Usuario = this.usuarioForm.value;
    console.log(usuario)

    this.usuarioService.login(usuario).subscribe({
      next: dados => {
        console.log(dados);
        if(dados){
          localStorage.setItem("nomecompleto", dados.nome);
          localStorage.setItem("idUsuario", dados.usuarioId);
          this.router.navigateByUrl('/separacao');
        }
      },
      error: err => console.log(err)
    })
  }
}

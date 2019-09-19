package it.rentx.backend.controller;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.DeleteMapping;
import org.springframework.web.bind.annotation.RequestHeader;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import it.rentx.backend.models.frontendModel.Risposta;
import it.rentx.backend.repository.UtenteRepository;
import it.rentx.backend.service.UtenteService;

@RestController
@RequestMapping("/auth")
public class AuthController {
	
	@Autowired
	UtenteService utenteService;
	
	@Autowired
	UtenteRepository utenteRepository;

	@DeleteMapping("/logout")
	public ResponseEntity<Risposta> logout(@RequestHeader("Authorization") String token) {
		return ResponseEntity.ok().body(new Risposta("true", "Logout effettuato con successo."));
	}
}

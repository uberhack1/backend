using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UberHack.API.Contracts;
using UberHack.API.Entities;

namespace UberHack.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkNetController : ControllerBase
    {
        // Mock persona Bianca
        private int _codigoUsuarioLogado = 1;
        IBaseRepository<Usuario> _usuarioRepository;

        public WorkNetController(IBaseRepository<Usuario> usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Usuario>> ObterPossiveisConexoes()
        {
            Usuario usuarioLogado = ObterUsuarioLogado();

            IEnumerable<Usuario> possiveisConexoes = _usuarioRepository.GetAll()
                .Where(o => o.FaculdadeId == usuarioLogado.FaculdadeId || o.EmpresaId == usuarioLogado.EmpresaId);

            return base.Ok(possiveisConexoes);
                
        }

        private Usuario ObterUsuarioLogado()
        {
            return _usuarioRepository.Get(_codigoUsuarioLogado);
        }
    }
}

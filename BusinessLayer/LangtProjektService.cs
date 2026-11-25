
using System;
using System.Collections.Generic;
using System.Linq;
using DTO;
using DataAccess.Model;
using DataAccess.Repositories;

namespace BusinessLogic
{
    public class LangtProjektService
    {
        private readonly LangtProjektRepository _repository = new();
        private readonly ArbejdsopgaveRepository _arbejdsopgaveRepository = new();
        private readonly MedarbejderRepository _medarbejderRepository = new();
        private readonly KundeRepository _kundeRepository = new();
        private readonly ProjektBeregner _projektBeregner = new();

        public List<OpretProjektDTO> GetAlleProjekter()
        {
            var projekter = _repository.GetAlleProjekter();

            return projekter.Select(p => new OpretProjektDTO
            {
                ProjektID = p.ProjektID,
                KundeID = p.Kundenummer,
                Adresse = p.Adress,
                Status = p.Status,
                ForventetVarighedTimer = p.ExpectedDurationHours,
                KvadratmeterAfTag = p.RoofSqrM,
                Startdate = p.Startdate,
                Endate = p.Endate,

                MedarbejderIds = p.Medarbejdere
                    .Select(m => m.ID)
                    .ToList(),

                Arbejdsopgaver = p.Arbejdsopgaver
                    .Select(a => new ValgtArbejdsopgaveDTO
                    {
                        ArbejdsopgaveID = a.ArbejdsopgaveID,
                        Maengde = a.Maengde,
                        ManualTimeHours = a.ManualTimeHours
                    })
                    .ToList()
            }).ToList();
        }

        public double OpretNytProjekt(OpretProjektDTO nytProjektFraUI)
        {
            KontrollerKunde(nytProjektFraUI.KundeID);

            var medarbejdere = HentValgteMedarbejdere(
                nytProjektFraUI.MedarbejderIds);

            var beregning = BeregnProjekt(
                nytProjektFraUI.Arbejdsopgaver,
                medarbejdere,
                nytProjektFraUI.Startdate);

            var langtProjekt = new LangtProjekt
            {
                Kundenummer = nytProjektFraUI.KundeID,
                Adress = nytProjektFraUI.Adresse,
                Status = nytProjektFraUI.Status,
                RoofSqrM = nytProjektFraUI.KvadratmeterAfTag,
                Startdate = nytProjektFraUI.Startdate.Date,
                Endate = beregning.Slutdato,
                ExpectedDurationHours = beregning.ForventetVarighedTimer,
                Medarbejdere = medarbejdere,
                Arbejdsopgaver = beregning.Arbejdsopgaver
            };

            _repository.GemProjekt(langtProjekt);

            return beregning.ForventetVarighedTimer;
        }

        public void OpdaterProjekt(OpretProjektDTO projektFraUI)
        {
            KontrollerKunde(projektFraUI.KundeID);

            var eksisterendeProjekt =
                _repository.GetProjekt(projektFraUI.ProjektID);

            if (eksisterendeProjekt == null)
                return;

            var medarbejdere = HentValgteMedarbejdere(
                projektFraUI.MedarbejderIds);

            var beregning = BeregnProjekt(
                projektFraUI.Arbejdsopgaver,
                medarbejdere,
                projektFraUI.Startdate);

            eksisterendeProjekt.Kundenummer = projektFraUI.KundeID;
            eksisterendeProjekt.Adress = projektFraUI.Adresse;
            eksisterendeProjekt.Status = projektFraUI.Status;
            eksisterendeProjekt.RoofSqrM = projektFraUI.KvadratmeterAfTag;
            eksisterendeProjekt.Startdate = projektFraUI.Startdate.Date;
            eksisterendeProjekt.Endate = beregning.Slutdato;
            eksisterendeProjekt.ExpectedDurationHours =
                beregning.ForventetVarighedTimer;

            eksisterendeProjekt.Medarbejdere = medarbejdere;
            eksisterendeProjekt.Arbejdsopgaver =
                beregning.Arbejdsopgaver;

            _repository.OpdaterProjekt(eksisterendeProjekt);
        }


        public ProjektBeregningResultat BeregnForhaandsvisning(
            List<int> medarbejderIds,
            List<ValgtArbejdsopgaveDTO> valgteOpgaver,
            DateTime startdato)
        {
            var medarbejdere = HentValgteMedarbejdere(medarbejderIds);

            return BeregnProjekt(
                valgteOpgaver,
                medarbejdere,
                startdato);
        }
    

        private ProjektBeregningResultat BeregnProjekt(
            List<ValgtArbejdsopgaveDTO> valgteOpgaver,
            List<Medarbejder> medarbejdere,
            DateTime startdato)
        {
            var arbejdsopgaver = valgteOpgaver
                .Select(a => _arbejdsopgaveRepository
                    .GetArbejdsopgave(a.ArbejdsopgaveID))
                .OfType<Arbejdsopgave>()
                .ToList();

            if (arbejdsopgaver.Count != valgteOpgaver.Count)
            {
                throw new InvalidOperationException(
                    "En eller flere valgte arbejdsopgaver blev ikke fundet.");
            }

            return _projektBeregner.Beregn(
                valgteOpgaver,
                arbejdsopgaver!,
                medarbejdere,
                startdato);
        }

        private List<Medarbejder> HentValgteMedarbejdere(
            List<int> medarbejderIds)
        {
            var unikkeIds = medarbejderIds.Distinct().ToList();

            var medarbejdere = _medarbejderRepository
                .GetAlleMedarbejdere()
                .Where(m => unikkeIds.Contains(m.ID))
                .ToList();

            if (medarbejdere.Count != unikkeIds.Count)
            {
                throw new InvalidOperationException(
                    "En eller flere valgte medarbejdere blev ikke fundet.");
            }

            return medarbejdere;
        }

        private void KontrollerKunde(int kundenummer)
        {
            bool kundeFindes = _kundeRepository
                .GetAlleKunder()
                .Any(k => k.Kundenummer == kundenummer);

            if (!kundeFindes)
            {
                throw new InvalidOperationException(
                    "Den valgte kunde blev ikke fundet.");
            }
        }

        public void SletProjekt(int id)
        {
            _repository.SletProjekt(id);
        }
    }
}

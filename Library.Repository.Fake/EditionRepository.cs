
using AutoMapper;
using Library.Dto;
using Library.Entities;
using Library.Interfaces.Repository;
using System;
using System.Collections.Generic;

namespace Library.Repository.Fake
{
    public class EditionRepository : IEditionRepository
    {
        private readonly List<Edition> _editions = new List<Edition>();
        private readonly IMapper _mapper;

        public EditionRepository(IMapper mapper)
        {
            _mapper = mapper;
        }

        public void DeleteEdition(int editionId)
        {
            foreach (var item in _editions)
            {
                if (item.EditionId == editionId)
                {
                    _editions.Remove(item);
                }
            }
        }

        public int GetEditionCount()
        {
            throw new NotImplementedException();
        }

        public List<EditionDto> GetEditions()
        {
            throw new NotImplementedException();
        }

        public List<EditionDto> GetEditionsByDate()
        {
            return _mapper.Map<List<EditionDto>>(_editions);
        }

        public List<EditionDto> GetEditionsByDateDesc()
        {
            throw new NotImplementedException();
        }

        public List<EditionDto> GetEditionsInPage(int pg)
        {
            throw new NotImplementedException();
        }

        public int InsertEdition(EditionDto editionDto)
        {
            editionDto.EditionId = _editions.Count;
            _editions.Add(_mapper.Map<Edition>(editionDto));
            return _editions.Count;
        }

        public void UpdateEdition(EditionDto editionDto)
        {
            throw new NotImplementedException();
        }

        int IEditionRepository.DeleteEdition(int editionId)
        {
            throw new NotImplementedException();
        }

        int IEditionRepository.UpdateEdition(EditionDto editionDto)
        {
            throw new NotImplementedException();
        }
    }
}

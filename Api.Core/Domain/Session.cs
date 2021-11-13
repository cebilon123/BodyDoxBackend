using System;
using System.Collections.Generic;
using Api.Core.Const;
using Api.Core.Exceptions.Session;

namespace Api.Core.Domain
{
    public class Session
    {
        public Guid Id { get; }
        public Guid ClientId { get; }
        public List<Image> Images { get; }
        public DateTime CreatedDate { get; }
        public DateTime? UpdatedDate { get; }

        public Session(Guid id, Guid clientId, List<Image> images, DateTime createdDate, DateTime? updatedDate)
        {
            Id = id;
            ClientId = clientId;
            Images = images;
            CreatedDate = createdDate;
            UpdatedDate = updatedDate;
        }
    }

    public class Image
    {
        public Guid Id { get; }
        public Side Side { get; }
        public Resolution Resolution { get; }

        public List<Annotation> Annotations { get; }

        public Image(Guid id, Resolution resolution, List<Annotation> annotations, Side side = Side.Front)
        {
            Id = id;
            Side = side;
            Resolution = resolution;
            Annotations = annotations;
        }
    }

    public class Annotation
    {
        public Guid Id { get; }
        public AnnotationType Type { get; }

        public Position MarkerStart { get; }
        public Position MarkerEnd { get; }
        public Position Position { get; }

        public decimal Rotation { get; }
        public decimal Scale { get; }

        public Annotation(Guid id, AnnotationType type, Position markerStart, Position markerEnd, Position position, decimal rotation = 0, decimal scale = 0)
        {
            Id = id;
            Type = type;
            MarkerStart = markerStart ?? throw new EmptyPositionException(nameof(markerStart));
            MarkerEnd = markerEnd ?? throw new EmptyPositionException(nameof(markerEnd));
            Position = position ?? throw new EmptyPositionException(nameof(position));
            Rotation = rotation;
            Scale = scale;
        }
    }

    public class Position
    {
        public int X { get; set; }
        public int Y { get; set; }
    }

    public class Resolution
    {
        public int Width { get; set; }
        public int Height { get; set; }
    }
}
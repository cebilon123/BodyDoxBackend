using System;
using System.Collections.Generic;
using Api.Core.Const;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
#pragma warning disable 8618

namespace Api.Infrastructure.Repositories.Documents
{
    public class SessionDocument : IIdentifiable<Guid>
    {
        public Guid Id { get; set; }
        public Guid ClientId { get; set; }
        public List<Image> ImagesData { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }

    public class Image
    {
        public Guid Id { get; set; }
        [BsonRepresentation(BsonType.Int32)]
        public Side Side { get; set; }
        public Resolution Resolution { get; set; }
        
        public List<Annotation> Annotations { get; set; }
    }
    
    public class Annotation
    {
        [BsonRepresentation(BsonType.Binary)]
        public Guid Id { get; set; }
        [BsonRepresentation(BsonType.Int32)]
        public AnnotationType Type { get; set; }
        
        public Position? MarkerStart { get; set; }
        public Position? MarkerEnd { get; set; }
        public Position? Position { get; set; }
        
        [BsonRepresentation(BsonType.Decimal128)]
        public decimal Rotation { get; set; }
        [BsonRepresentation(BsonType.Decimal128)]
        public decimal Scale { get; set; }
    }

    /// <summary>
    /// Position of one of the sides of the annotation
    /// </summary>
    public class Position
    {
        [BsonRepresentation(BsonType.Int64)]
        public int X { get; set; }
        [BsonRepresentation(BsonType.Int64)]
        public int Y { get; set; }
    }

    /// <summary>
    /// We are creating annotations according to position on the image, so resolution is useful information if it comes
    /// to change something in image.
    /// </summary>
    public class Resolution
    {
        [BsonRepresentation(BsonType.Int32)]
        public int Width { get; set; }
        [BsonRepresentation(BsonType.Int32)]
        public int Height { get; set; }
    }
}
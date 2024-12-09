using System.Globalization;
using System.Text;

namespace POD.Common.Utils.ThreeDimGeometry
{
    
    public class BoundingBox
    {
        public double MinX { get; set; } = double.MaxValue;
        public double MaxX { get; set; } = double.MinValue;
        public double MinY { get; set; } = double.MaxValue;
        public double MaxY { get; set; } = double.MinValue;
        public double MinZ { get; set; } = double.MaxValue;
        public double MaxZ { get; set; } = double.MinValue;

        public void Update(double x, double y, double z)
        {
            if (x < MinX) MinX = x;
            if (x > MaxX) MaxX = x;
            if (y < MinY) MinY = y;
            if (y > MaxY) MaxY = y;
            if (z < MinZ) MinZ = z;
            if (z > MaxZ) MaxZ = z;
        }
        
        public string ToJson()
        {
            return $"{{\"MinX\": {MinX}, \"MaxX\": {MaxX}, \"MinY\": {MinY}, \"MaxY\": {MaxY}, \"MinZ\": {MinZ}, \"MaxZ\": {MaxZ}}}";
        }
    }

    public class StlParser
    {
        public static BoundingBox CalculateBoundingBoxFromAscii(byte[] stlBytes)
        {
            BoundingBox bbox = new BoundingBox();

            using (var memoryStream = new MemoryStream(stlBytes))
            using (var reader = new StreamReader(memoryStream, Encoding.ASCII))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    line = line.Trim();
                    if (line.StartsWith("vertex", StringComparison.OrdinalIgnoreCase))
                    {
                        string[] parts = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        double x = double.Parse(parts[1], CultureInfo.InvariantCulture);
                        double y = double.Parse(parts[2], CultureInfo.InvariantCulture);
                        double z = double.Parse(parts[3], CultureInfo.InvariantCulture);

                        bbox.Update(x, y, z);
                    }
                }
            }

            return bbox;
        }
        
        public static BoundingBox CalculateBoundingBoxFromBinary(byte[] stlBytes)
        {
            BoundingBox bbox = new BoundingBox();

            using (var memoryStream = new MemoryStream(stlBytes))
            using (var binaryReader = new BinaryReader(memoryStream))
            {
                binaryReader.ReadBytes(80); // Skip the header
                uint triangleCount = binaryReader.ReadUInt32();

                for (int i = 0; i < triangleCount; i++)
                {
                    // Ignore normal vectors
                    binaryReader.ReadSingle();
                    binaryReader.ReadSingle();
                    binaryReader.ReadSingle();

                    // Read vertices and update bounding box
                    for (int j = 0; j < 3; j++)
                    {
                        float x = binaryReader.ReadSingle();
                        float y = binaryReader.ReadSingle();
                        float z = binaryReader.ReadSingle();
                        bbox.Update(x, y, z);
                    }

                    binaryReader.ReadBytes(2); // Skip attribute byte count
                }
            }

            return bbox;
        }

        
        public static double[][] ParseVerticesFromBinary(byte[] stlBytes)
        {
            if (stlBytes == null)
            {
                throw new ArgumentNullException(nameof(stlBytes), "Input byte array cannot be null.");
            }

            using (var memoryStream = new MemoryStream(stlBytes))
            using (var binaryReader = new BinaryReader(memoryStream))
            {
                if (memoryStream.Length < 84) // Minimum length to have a header and triangle count
                {
                    throw new ArgumentException("STL file is too short to be valid.", nameof(stlBytes));
                }

                // Read and validate the header
                var headerBytes = binaryReader.ReadBytes(80);
                if (Encoding.UTF8.GetString(headerBytes).Contains("\0\0\0\0\0\0\0\0"))
                {
                    throw new ArgumentException("Invalid STL file header. Possible corrupted file or incorrect format.", nameof(stlBytes));
                }

                // Read the number of triangles
                uint triangleCount;
                try
                {
                    triangleCount = binaryReader.ReadUInt32();
                }
                catch (EndOfStreamException)
                {
                    throw new ArgumentException("Unexpected end of stream when reading triangle count.", nameof(stlBytes));
                }

                if (memoryStream.Length < 84 + triangleCount * 50) // Each triangle has 50 bytes of data
                {
                    throw new ArgumentException("STL file does not match the expected size based on its triangle count.", nameof(stlBytes));
                }

                double[][] vertices = new double[triangleCount * 3][];

                try
                {
                    for (int i = 0; i < triangleCount; i++)
                    {
                        // Read and ignore the normal vector
                        binaryReader.ReadSingle(); // Normal X
                        binaryReader.ReadSingle(); // Normal Y
                        binaryReader.ReadSingle(); // Normal Z

                        // Read vertices
                        vertices[i * 3 + 0] = new double[] { binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle() };
                        vertices[i * 3 + 1] = new double[] { binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle() };
                        vertices[i * 3 + 2] = new double[] { binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle() };

                        // Skip attribute byte count
                        binaryReader.ReadBytes(2);
                    }
                }
                catch (EndOfStreamException)
                {
                    throw new ArgumentException("Unexpected end of stream when reading triangle data.", nameof(stlBytes));
                }

                return vertices;
            }
        }
        
        public static double ParseAndCalculateVolumeFromBinary(byte[] stlBytes)
        {
            if (stlBytes == null)
            {
                throw new ArgumentNullException(nameof(stlBytes), "Input byte array cannot be null.");
            }

            using (var memoryStream = new MemoryStream(stlBytes))
            using (var binaryReader = new BinaryReader(memoryStream))
            {
                if (memoryStream.Length < 84) // Minimum length to have a header and triangle count
                {
                    throw new ArgumentException("STL file is too short to be valid.", nameof(stlBytes));
                }

                // Read and validate the header
                binaryReader.ReadBytes(80); // Skip the header

                uint triangleCount = binaryReader.ReadUInt32();

                if (memoryStream.Length < 84 + triangleCount * 50) // Each triangle has 50 bytes of data
                {
                    throw new ArgumentException("STL file does not match the expected size based on its triangle count.", nameof(stlBytes));
                }

                double totalVolume = 0.0;

                try
                {
                    for (int i = 0; i < triangleCount; i++)
                    {
                        // Read and ignore the normal vector
                        binaryReader.ReadSingle(); // Normal X
                        binaryReader.ReadSingle(); // Normal Y
                        binaryReader.ReadSingle(); // Normal Z

                        // Read vertices
                        double[] v1 = new double[] { binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle() };
                        double[] v2 = new double[] { binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle() };
                        double[] v3 = new double[] { binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle() };

                        // Calculate volume contribution of this triangle
                        totalVolume += CalculateTetrahedronVolume(v1, v2, v3);

                        // Skip attribute byte count
                        binaryReader.ReadBytes(2);
                    }
                }
                catch (EndOfStreamException)
                {
                    throw new ArgumentException("Unexpected end of stream when reading triangle data.", nameof(stlBytes));
                }

                return totalVolume;
            }
        }
        
        public static double CalculateVolumeFromBinary(byte[] stlBytes)
        {
            if (stlBytes == null)
            {
                throw new ArgumentNullException(nameof(stlBytes), "Input byte array cannot be null.");
            }

            double totalVolume = 0;
            using (var memoryStream = new MemoryStream(stlBytes))
            using (var binaryReader = new BinaryReader(memoryStream))
            {
                if (memoryStream.Length < 84) // Minimum length to have a header and triangle count
                {
                    throw new ArgumentException("STL file is too short to be valid.", nameof(stlBytes));
                }

                binaryReader.ReadBytes(80); // Skip the header

                uint triangleCount = binaryReader.ReadUInt32();

                if (memoryStream.Length < 84 + triangleCount * 50) // Each triangle has 50 bytes of data
                {
                    throw new ArgumentException("STL file does not match the expected size based on its triangle count.", nameof(stlBytes));
                }

                try
                {
                    for (int i = 0; i < triangleCount; i++)
                    {
                        // Ignore normal vector
                        binaryReader.ReadSingle();
                        binaryReader.ReadSingle();
                        binaryReader.ReadSingle();

                        // Read vertices
                        float x1 = binaryReader.ReadSingle();
                        float y1 = binaryReader.ReadSingle();
                        float z1 = binaryReader.ReadSingle();
                        float x2 = binaryReader.ReadSingle();
                        float y2 = binaryReader.ReadSingle();
                        float z2 = binaryReader.ReadSingle();
                        float x3 = binaryReader.ReadSingle();
                        float y3 = binaryReader.ReadSingle();
                        float z3 = binaryReader.ReadSingle();

                        // Volume calculation part
                        double signedVolume = (x1 * (y2 * z3 - z2 * y3)
                                             + x2 * (y3 * z1 - z3 * y1)
                                             + x3 * (y1 * z2 - z1 * y2)) / 6.0;
                        totalVolume += signedVolume;

                        // Skip attribute byte count
                        binaryReader.ReadBytes(2);
                    }
                }
                catch (EndOfStreamException)
                {
                    throw new ArgumentException("Unexpected end of stream when reading triangle data.", nameof(stlBytes));
                }

                return Math.Abs(totalVolume); // Return the absolute value of the total volume
            }
        }
        
        private static double CalculateTetrahedronVolume(double[] v1, double[] v2, double[] v3)
        {
            return Math.Abs(
                v1[0] * (v2[1] * v3[2] - v3[1] * v2[2]) +
                v2[0] * (v3[1] * v1[2] - v1[1] * v3[2]) +
                v3[0] * (v1[1] * v2[2] - v2[1] * v1[2])
            ) / 6.0;
        }
        
        public static double CalculateVolumeFromAscii(byte[] stlBytes)
        {
            if (stlBytes == null)
            {
                throw new ArgumentNullException(nameof(stlBytes), "Input byte array cannot be null.");
            }

            double totalVolume = 0;
            using (var memoryStream = new MemoryStream(stlBytes))
            using (var reader = new StreamReader(memoryStream, Encoding.ASCII))
            {
                string line;
                double[] vertex1 = null, vertex2 = null, vertex3 = null;
                int vertexCount = 0;

                try
                {
                    while ((line = reader.ReadLine()) != null)
                    {
                        line = line.Trim();
                        if (line.StartsWith("vertex", StringComparison.OrdinalIgnoreCase))
                        {
                            string[] parts = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            if (parts.Length >= 4 && double.TryParse(parts[1], NumberStyles.Float, CultureInfo.InvariantCulture, out double x)
                                && double.TryParse(parts[2], NumberStyles.Float, CultureInfo.InvariantCulture, out double y)
                                && double.TryParse(parts[3], NumberStyles.Float, CultureInfo.InvariantCulture, out double z))
                            {
                                if (vertexCount == 0)
                                    vertex1 = new double[] { x, y, z };
                                else if (vertexCount == 1)
                                    vertex2 = new double[] { x, y, z };
                                else if (vertexCount == 2)
                                    vertex3 = new double[] { x, y, z };

                                vertexCount++;
                            }
                        }
                        else if (line.StartsWith("endloop", StringComparison.OrdinalIgnoreCase))
                        {
                            if (vertexCount != 3 || vertex1 == null || vertex2 == null || vertex3 == null)
                                throw new InvalidOperationException("Incomplete triangle data or parsing error before endloop.");

                            totalVolume += CalculateTetrahedronVolume(vertex1, vertex2, vertex3);
                            vertexCount = 0;
                        }
                    }
                }
                catch (Exception e)
                {
                    throw new ArgumentException("Error while parsing ASCII STL file: " + e.Message, nameof(stlBytes));
                }

                return Math.Abs(totalVolume); // Ensure non-negative volume regardless of triangle orientation
            }
        }
        
        public static bool IsAsciiStl(byte[] fileBytes)
        {
            // Check for minimum length for a binary STL header + size of uint32 (84 bytes)
            if (fileBytes.Length < 84)
            {
                throw new ArgumentException("File too short to be a valid STL file.");
            }

            // Read the first 80 bytes as a header
            string header = Encoding.ASCII.GetString(fileBytes, 0, 80);

            // Simple check for ASCII STL which typically starts with "solid"
            if (header.StartsWith("solid"))
            {
                // Further check to rule out binary files that misleadingly start with "solid"
                // Check for non-ASCII characters in the first chunk after the header
                for (int i = 80; i < fileBytes.Length && i < 134; i++)
                {
                    if (fileBytes[i] > 127) // Beyond ASCII range
                        return false;
                }

                // Assume it's ASCII if no non-ASCII character is found
                return true;
            }

            return false;
        }
    }

    public class ObjParser
    {
        
    }
}
